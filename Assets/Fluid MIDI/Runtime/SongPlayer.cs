using FluidSynth;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using UnityEngine;

namespace FluidMidi
{
    public class SongPlayer : MonoBehaviour
    {
        static readonly ISet<SongPlayer> players = new HashSet<SongPlayer>();

        public static void PauseAll()
        {
            foreach (SongPlayer player in players)
            {
                player.Pause();
            }
        }

        public static void ResumeAll()
        {
            foreach (SongPlayer player in players)
            {
                player.Resume();
            }
        }

        public static void StopAll()
        {
            foreach (SongPlayer player in players)
            {
                player.Stop();
            }
        }

        struct PrepareJob : IJob
        {
            [NativeDisableUnsafePtrRestriction]
            [ReadOnly]
            readonly IntPtr playerPtr;

            public PrepareJob(IntPtr playerPtr)
            {
                this.playerPtr = playerPtr;
            }

            public void Execute()
            {
                Api.Player.Prepare(playerPtr);
            }
        }

        IntPtr synthPtr = IntPtr.Zero;
        IntPtr playerPtr = IntPtr.Zero;
        JobHandle prepareJob;
        IntPtr driver = IntPtr.Zero;
        float unloadDelay = -1;

        [SerializeField]
        public Synthesizer synthesizer;
        //[SerializeField]
        //public StreamingAsset song = new StreamingAsset();
        public string songPath;
        [SerializeField]
        [Tooltip("Start playing after the song is loaded for the first time.")]
        public bool playOnStart = true;
        [SerializeField]
        [Tooltip("Automatically unload the song when it is stopped.")]
        [ToggleIntFoldout(name = "Delay", tooltip = "Seconds to wait for notes to finish playing.")]
        ToggleInt unloadOnStop = new ToggleInt(true, 3);
        [SerializeField]
        [Tooltip("Play again when the song reaches the end.")]
        [ToggleIntFoldout(name = "Start Ticks", tooltip = "Position to start playing again.")]
        ToggleInt loop = new ToggleInt(false, 0);
        [SerializeField]
        [Tooltip("Make the song end early.")]
        ToggleInt endTicks = new ToggleInt(false, 0);
        [SerializeField]
        [Range(0, 10)]
        public float gain = 0.2f;

        /// <summary>
        /// Start playing the song from the beginning.
        /// </summary>
        /// <remarks>
        /// Will load the song if necessary.
        /// </remarks>
        public void Play()
        {
            enabled = true;
            if (playerPtr == IntPtr.Zero)
            {
                Logger.LogError("Play called before SongPlayer initialized! " +
                    "Call from Start() or later or adjust script execution priority.");
                return;
            }
            if (!IsPlaying)
            {
                Api.Player.Seek(playerPtr, 0);
                Api.Player.Play(playerPtr);
                unloadDelay = unloadOnStop.Value;
                IsPaused = false;
            }
        }

        public void Stop()
        {
            if (IsPlaying)
            {
                Api.Player.Stop(playerPtr);
                IsPaused = false;
            }
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                Api.Player.Stop(playerPtr);
                IsPaused = true;
            }
        }

        /// <summary>
        /// Starts playing again.
        /// </summary>
        /// <remarks>
        /// This has no effect unless the song is paused.
        /// </remarks>
        public void Resume()
        {
            if (IsPaused)
            {
                Api.Player.Play(playerPtr);
                IsPaused = false;
            }
        }

        /// <summary>
        /// Start playing from a new position.
        /// </summary>
        /// <remarks>
        /// If the song is not playing, it will begin playing from the new position
        /// when playback starts. This function will fail if the SongPlayer is not enabled
        /// or during playback before a previous call to seek has taken effect.
        /// </remarks>
        /// <param name="ticks">The new position in ticks.</param>
        /// <returns>
        /// True if the new position was set successfully.
        /// </returns>
        public bool Seek(int ticks)
        {
            return playerPtr != IntPtr.Zero && Api.Player.Seek(playerPtr, ticks) == Api.Result.OK;
        }

        public int Ticks
        {
            get
            {
                return playerPtr != IntPtr.Zero ? Api.Player.GetCurrentTick(playerPtr) : 0;
            }
        }

        /// <summary>
        /// True if the song is loaded and ready to play.
        /// </summary>
        public bool IsReady
        {
            get
            {
                return driver != IntPtr.Zero;
            }
        }

        /// <summary>
        /// True if the song is playing.
        /// </summary>
        /// <remarks>
        /// A paused song is still considered playing.
        /// </remarks>
        public bool IsPlaying
        {
            get
            {
                return IsPaused || (playerPtr != IntPtr.Zero && Api.Player.GetStatus(playerPtr) == Api.Player.Status.Playing);
            }
        }

        /// <summary>
        /// True if the song is paused.
        /// </summary>
        /// <remarks>
        /// A paused song is still considered playing.
        /// </remarks>
        public bool IsPaused { get; private set; }

        /// <summary>
        /// True if the song has finished playing or was stopped.
        /// </summary>
        public bool IsDone
        {
            get
            {
                return unloadDelay >= 0 && !IsPlaying;
            }
        }

        void Awake()
        {
            players.Add(this);
        }

        void OnEnable()
        {
            if (synthesizer == null)
            {
                Logger.LogError("No synthesizer specified");
                enabled = false;
                return;
            }
            Logger.AddReference();
            Settings.AddReference();
            synthesizer.AddReference();
            synthPtr = Api.Synth.Create(Settings.Ptr);
            Api.Synth.SetGain(synthPtr, gain);
            playerPtr = Api.Player.Create(synthPtr);
            //string songPath = song.GetFullPath();
            if (songPath.Length > 0)
            {
                if (File.Exists(songPath))
                {
                    //Api.Player.Add(playerPtr, song.GetFullPath());
                    Api.Player.Add(playerPtr, songPath);
                }
                else
                {
                    Logger.LogError("Song file missing: " + songPath);
                }
            }
            else
            {
                Logger.LogError("No song specified");
            }
            if (loop.Enabled)
            {
                Api.Player.SetLoop(playerPtr, -1);
                if (loop.Value > 0)
                {
                    Api.Player.SetLoopBegin(playerPtr, loop.Value);
                }
            }
            Api.Player.SetEnd(playerPtr, endTicks.Enabled ? endTicks.Value : -1);
            Api.Player.Stop(playerPtr);
            prepareJob = new PrepareJob(playerPtr).Schedule();
            unloadDelay = -1;
        }

        void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        void Update()
        {
            if (unloadOnStop.Enabled && IsDone)
            {
                unloadDelay -= Time.unscaledDeltaTime;
                if (unloadDelay <= 0)
                {
                    unloadDelay = 0;
                    enabled = false;
                }
            }
            else if (driver == IntPtr.Zero && prepareJob.IsCompleted)
            {
                CreateDriver();
            }
        }

        void OnDisable()
        {
            if (synthesizer == null)
            {
                return;
            }
            IsPaused = false;
            if (driver != IntPtr.Zero)
            {
                Api.Driver.Destroy(driver);
                driver = IntPtr.Zero;
            }
            else if (!prepareJob.IsCompleted)
            {
                Logger.LogWarning("Disabling SongPlayer before prepared");
            }
            prepareJob.Complete();
            Api.Player.Destroy(playerPtr);
            playerPtr = IntPtr.Zero;
            Api.Synth.RemoveSoundFont(synthPtr, Api.Synth.GetSoundFont(synthPtr, 0));
            Api.Synth.Destroy(synthPtr);
            synthPtr = IntPtr.Zero;
            synthesizer.RemoveReference();
            Settings.RemoveReference();
            Logger.RemoveReference();
        }

        void OnDestroy()
        {
            players.Remove(this);
        }

        void Reset()
        {
            Synthesizer[] synthesizers = FindObjectsOfType<Synthesizer>();
            if (synthesizers.Length == 0)
            {
                synthesizer = gameObject.AddComponent<Synthesizer>();
            }
            else if (synthesizers.Length == 1)
            {
                synthesizer = synthesizers[0];
            }
        }

        void OnValidate()
        {
            if (loop.Value < 0)
            {
                loop.Value = 0;
            }
            if (endTicks.Value < 0)
            {
                endTicks.Value = 0;
            }
            //string songPath = song.GetFullPath();
            if (songPath.Length > 0 && Api.Misc.IsMidiFile(songPath) == 0)
            {
                Logger.LogError("Not a MIDI file: " + songPath);
                //song.SetFullPath(string.Empty);
            }
            if (synthPtr != IntPtr.Zero)
            {
                Api.Synth.SetGain(synthPtr, gain);
            }
        }

        void CreateDriver()
        {
            if (synthesizer.SoundFontPtr != IntPtr.Zero)
            {
                if (Api.Synth.SoundFontCount(synthPtr) == 0)
                {
                    Api.Synth.AddSoundFont(synthPtr, synthesizer.SoundFontPtr);
                }
                driver = Api.Driver.Create(Settings.Ptr, synthPtr);
            }
        }
    }
}