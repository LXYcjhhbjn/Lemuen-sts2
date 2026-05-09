using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ShaoBingB: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<AMMO>(),
		HoverTipFactory.FromCard<ShaoBingBmk>()
	];
    public ShaoBingB() : base(1, CardType.Skill, CardRarity.Common, TargetType.Self,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
        await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<ShaoBingBmk>(base.Owner), PileType.Hand, cardPlay.Card.Owner);
    }
	protected override void OnUpgrade()
	{
       base.DynamicVars["AMMO"].UpgradeValueBy(1m);
	}
}
