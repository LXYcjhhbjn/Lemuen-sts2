using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class LunYiC: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BufferPower>(1m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<LunYiCmk>(),
        HoverTipFactory.FromPower<BufferPower>()
	];
    public LunYiC() : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<BufferPower>(choiceContext , base.Owner.Creature, base.DynamicVars["BufferPower"].BaseValue, base.Owner.Creature, this);
        await CardPileCmd.AddGeneratedCardToCombat(base.CombatState.CreateCard<LunYiCmk>(base.Owner), PileType.Hand, cardPlay.Card.Owner);
    }
	protected override void OnUpgrade()
	{
        base.EnergyCost.UpgradeBy(-1);
	}
}
