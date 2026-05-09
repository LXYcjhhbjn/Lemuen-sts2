using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(TokenCardPool))]
public class QiangXieAmk: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AIM>(2m)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Unplayable,MyKeywords.Modification];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<AIM>()];
    public QiangXieAmk() : base(-1, CardType.Skill, CardRarity.Common, TargetType.None ,true){}
    protected override void OnUpgrade()
    {
        base.DynamicVars["AIM"].UpgradeValueBy(1m);
    }
	public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext,CombatSide side)
	{
		CardPile?pile = base.Pile;
		if (pile != null && pile.Type != PileType.Exhaust && side == base.Owner.Creature.Side)
		{
			await CardPileCmd.Add(this,PileType.Exhaust);
            await PowerCmd.Apply<AIM>(choiceContext , base.Owner.Creature, base.DynamicVars["AIM"].BaseValue, base.Owner.Creature, this);
		}
	}
}
