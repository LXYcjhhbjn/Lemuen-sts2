using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZhuiJi: CustomCardModel
{
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AIM>(),
        HoverTipFactory.FromPower<TongJi>()
    ];
    public ZhuiJi() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self)
    {
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
        await PowerCmd.Apply<AIM>(choiceContext, base.Owner.Creature, cardPlay.Card.CombatState?.Enemies.Where((Creature c) => c.IsAlive).Sum((Creature c) => c.GetPowerAmount<TongJi>()) ?? 0 , base.Owner.Creature, this);
	}
	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}