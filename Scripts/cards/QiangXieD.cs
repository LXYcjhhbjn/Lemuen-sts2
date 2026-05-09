using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QiangXieD: CustomCardModel
{
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<QiangXieDmk>()
	];
    public QiangXieD() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<QiangXieDmk>(base.Owner), PileType.Hand, cardPlay.Card.Owner);
    }
	protected override void OnUpgrade()
	{
        base.EnergyCost.UpgradeBy(-1);
	}
}
