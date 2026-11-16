using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace HoshimiMiyabi.Other
{
	public partial class FallenFrostCounter : ZZZSecondEnergyCounter
	{
		[Export]
		public Array<TextureRect> _frosts;
        [Export]
        public AnimatedSprite2D _fire;

		public override void _Ready()
		{
			base._Ready();
			OnEnergyChanged(1, 0);
		}

		protected override void Reflash()
		{
			
		}

		protected override void OnEnergyChanged(int oldEnergy, int newEnergy)
		{
			if(oldEnergy!=newEnergy)
			{
				for(int i=0;i<6;i++)
				{
					if (i < newEnergy) _frosts[i].Show();
					else _frosts[i].Hide();
				}
				if(newEnergy>=_frosts.Count)_fire.Show();
				else _fire.Hide();
			}
		}

	}

}
