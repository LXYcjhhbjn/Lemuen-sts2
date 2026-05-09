using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Saves.Runs;

namespace LXY.Scripts;

[Pool(typeof(lemuenRelicPool))]
public class AppleAndPlant : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Common;
    public override string PackedIconPath => $"res://lemuen/images/relics/AppleAndPlant.png";
    protected override string PackedIconOutlinePath => $"res://lemuen/images/relics/AppleAndPlant.png";
    protected override string BigIconPath => $"res://lemuen/images/relics/AppleAndPlant.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AMMO>()];
	private bool _isActivating;
	private int _SkillsPlayed;
	public override bool ShowCounter => true;
	public override int DisplayAmount
	{
		get
		{
			if (!IsActivating)
			{
				return SkillsPlayed % base.DynamicVars.Cards.IntValue;
			}
			return base.DynamicVars.Cards.IntValue;
		}
	}

	protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(5),new PowerVar<AMMO>(1)];
	private bool IsActivating
	{
		get
		{
			return _isActivating;
		}
		set
		{
			AssertMutable();
			_isActivating = value;
			UpdateDisplay();
		}
	}

	[SavedProperty]
	public int SkillsPlayed
	{
		get
		{
			return _SkillsPlayed;
		}
		set
		{
			AssertMutable();
			_SkillsPlayed = value;
			UpdateDisplay();
		}
	}

	private void UpdateDisplay()
	{
		if (IsActivating)
		{
			base.Status = RelicStatus.Normal;
		}
		else
		{
			int intValue = base.DynamicVars.Cards.IntValue;
			base.Status = ((SkillsPlayed % intValue == intValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
		}
		InvokeDisplayAmountChanged();
	}

	public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
	{
		if (cardPlay.Card.Owner == base.Owner && cardPlay.Card.Type == CardType.Skill)
		{
			SkillsPlayed++;
			int intValue = base.DynamicVars.Cards.IntValue;
			if (CombatManager.Instance.IsInProgress && SkillsPlayed % intValue == 0)
			{
				TaskHelper.RunSafely(DoActivateVisuals());
				await PowerCmd.Apply<AMMO>(context,base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, null);
			}
		}
	}

	private async Task DoActivateVisuals()
	{
		IsActivating = true;
		Flash();
		await Cmd.Wait(1f);
		IsActivating = false;
	}
}