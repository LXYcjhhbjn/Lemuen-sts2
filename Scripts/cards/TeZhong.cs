using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class TeZhong: CustomCardModel
{
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
        HoverTipFactory.FromPower<ChuanJiaDan>()
    ];
	public TeZhong() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self, true)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await PowerCmd.Apply<ChuanJiaDan>(choiceContext,base.Owner.Creature, base.Owner.Creature.GetPowerAmount<AMMO>() >> 1, base.Owner.Creature, this);
        await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, -(base.Owner.Creature.GetPowerAmount<AMMO>() >> 1), base.Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}