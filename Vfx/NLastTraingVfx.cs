using BaseLib.Audio;
using Godot;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Audio.Debug;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.Audio;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.TestSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace HoshimiMiyabi.Vfx
{
    public partial class NLastTraingVfx : Node2D
    {
        public static readonly string scenePath = "res://HoshimiMiyabi/last.tscn";

        [Export] private VideoStreamPlayer? _video;

        private CancellationTokenSource? _cts;

        public static float totalAnticipationDuration = 34f;

        public static NLastTraingVfx? Create()
        {
            if (TestMode.IsOn) return null;

            var scene = PreloadManager.Cache.GetScene(scenePath)
                .Instantiate<NLastTraingVfx>(PackedScene.GenEditState.Disabled);
            return scene;
        }

        public override void _Ready()
        {
            TaskHelper.RunSafely(PlaySequence());
        }

        public override void _ExitTree()
        {
            _cts?.Cancel();
        }

        private async Task PlaySequence()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            if (_video == null)
            {
                this.QueueFreeSafely();
                return;
            }

            //_video.GlobalPosition = GetViewportRect().Size / 2f;

            _video.Size = GetViewportRect().Size * 0.67f;

            _video.Bus = "Master";   // 主音量由总线控制
            _video.VolumeDb = Mathf.LinearToDb(SaveManager.Instance.SettingsSave.VolumeBgm * 0.75f);

            _video.Play();

            // 等视频播完；如果视频很长可用固定时长代替
            while (_video.IsPlaying())
            {
                await Cmd.Wait(0.05f, token); // 轮询，避免阻塞
                if (token.IsCancellationRequested) return;
            }

            this.QueueFreeSafely();
        }
    }
}
