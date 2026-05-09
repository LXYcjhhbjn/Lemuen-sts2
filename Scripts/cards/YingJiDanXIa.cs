using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class YingJiDanXia: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<YingJiDanXiaPower>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<YingJiDanXiaPower>()
    ];
	public YingJiDanXia()
		: base(2, CardType.Power, CardRarity.Rare, TargetType.Self, true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, 3m, base.Owner.Creature, this);
		await PowerCmd.Apply<YingJiDanXiaPower>(choiceContext,base.Owner.Creature, base.DynamicVars["YingJiDanXiaPower"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}