using BaseLib.Audio;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;
using ZZZLib.Other;

namespace HoshimiMiyabi.Other
{
    public static class MiyabiVoiceManager
    {
        static readonly string voicePath = "res://HoshimiMiyabi/voice/";

        static readonly string bigSkill = "大";

        static readonly int bigSkillN = 3;

        static readonly string attackVoice = "攻";

        static readonly int attackVoiceN = 3;

        static readonly string shuangyueVoice = "霜月";

        static readonly int shuangyueVoiceN = 3;

        static readonly string xianVoice = "闲";

        static readonly int xianVoiceN = 4;

        static readonly string selectVoice = "选";

        static readonly int selectVoiceN = 2;

        static readonly string chargeVoice = "蓄";

        private static readonly Random _rng = new();

        public static int GetRandomN(int n)
        {
            return _rng.Next(n);
        }

        public static string GetLanguage()
        {
            return ZZZConfig.VoiceLanguage == ZZZConfig.Language.Chinese ? "中" : "日";
        }

        public static void PlayBigSkillVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;
            int n = GetRandomN(bigSkillN) + 1;
            ModAudio.PlaySound(new ModSound(
                voicePath + bigSkill + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayBigSkillVfx()
        {
            ModAudio.PlaySound(new ModSound(
                voicePath + "大招"+ ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }


        public static void PlayAttackVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(attackVoiceN + 2) + 1;
            if (n <= attackVoiceN)

                ModAudio.PlaySound(new ModSound(
                voicePath + attackVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayShuangyueVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(shuangyueVoiceN) + 1;
            ModAudio.PlaySound(new ModSound(
                voicePath + shuangyueVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayXianVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(xianVoiceN) + 1;
            ModAudio.PlaySound(new ModSound(
                voicePath + xianVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlaySelectVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(selectVoiceN) + 1;
            ModAudio.PlaySound(new ModSound(
                voicePath + selectVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 9);
        }

        public static async Task PlayChargeVoice(int n,Player player,CardModel cardModel)
        {
            if (!ZZZConfig.CharacterVoice ||
                n<1 || n>3) return;

            ModAudio.PlaySound(new ModSound(
                voicePath + chargeVoice + n + ".mp3",
                ModAudio.SoundType.Sfx), -2);

            for(int i = 0; i < n; i++)
            {
                await ZZZCmd.AddSecondEnergy(player, -2, player.Creature, cardModel);
                await Cmd.Wait(0.22f);
            }
        }
    }
}
