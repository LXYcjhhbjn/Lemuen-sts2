using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QiangXieB: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(4m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<QiangXieBmk>(),
        HoverTipFactory.FromPower<AIM>()
	];
    public QiangXieB() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AIM>(choiceContext , base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
        await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<QiangXieBmk>(base.Owner), PileType.Hand, cardPlay.Card.Owner);
    }
	protected override void OnUpgrade()
	{
        base.DynamicVars["AIM"].UpgradeValueBy(2m);
	}
}
