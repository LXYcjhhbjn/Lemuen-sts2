using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class TaoFanYinDuShouXu: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TaoFanYinDuShouXuPower>(3m),new PowerVar<AIM>(4m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
		HoverTipFactory.FromPower<AIM>(),
		HoverTipFactory.FromPower<TaoFanYinDuShouXuPower>()
    ];
	public TaoFanYinDuShouXu()
		: base(1, CardType.Power, CardRarity.Rare, TargetType.Self, true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, 1m, base.Owner.Creature, this);
        await PowerCmd.Apply<AIM>(choiceContext,base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
		await PowerCmd.Apply<TaoFanYinDuShouXuPower>(choiceContext,base.Owner.Creature, base.DynamicVars["TaoFanYinDuShouXuPower"].BaseValue, base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.DynamicVars["TaoFanYinDuShouXuPower"].UpgradeValueBy(1m);
	}
}
