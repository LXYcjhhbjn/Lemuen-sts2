using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ZouHuo: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    public override int MaxUpgradeLevel => 999;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain,MyKeywords.Guns];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(8, ValueProp.Move),
        new ExtraDamageVar(3m)
    ];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
        HoverTipFactory.FromKeyword(MyKeywords.Modification)
    ];
    public ZouHuo() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy,true)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack).ToList();
		int cardCount = list.Count;
		foreach (CardModel item in list)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>())
		{
			await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, -1 , base.Owner.Creature, this);
            int num=(from card in PileType.Exhaust.GetPile(base.Owner).Cards.ToList()where card.Keywords.Contains(MyKeywords.Modification) select card).Count()+(from card in PileType.Hand.GetPile(base.Owner).Cards.ToList()where card.Keywords.Contains(MyKeywords.Modification) select card).Count();
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue+DynamicVars.ExtraDamage.BaseValue*num).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
		}
    }
	protected override PileType GetResultPileTypeForCardPlay()
	{
		PileType resultPileType = base.GetResultPileTypeForCardPlay();
		if (resultPileType != PileType.Discard)
		{
			return resultPileType;
		}
		return PileType.Hand;
	}
    protected override void OnUpgrade()
    {
        base.DynamicVars.Damage.UpgradeValueBy(2);
        base.DynamicVars.ExtraDamage.UpgradeValueBy(1m);
        AddKeyword(CardKeyword.Innate);
    }
}