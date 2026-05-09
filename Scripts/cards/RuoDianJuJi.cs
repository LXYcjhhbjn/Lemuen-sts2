using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class RuoDianJuJi: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    public override int MaxUpgradeLevel => 999;
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Retain,MyKeywords.Guns];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7, ValueProp.Move)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
        HoverTipFactory.FromPower<StrengthPower>()
    ];
    public RuoDianJuJi() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy,true)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
		List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack).ToList();
		int cardCount = list.Count;
		foreach (CardModel item in list)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>())
		{
			await PowerCmd.Apply<AMMO>(choiceContext,base.Owner.Creature, -1 , base.Owner.Creature, this);
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).Execute(choiceContext);
            if (cardPlay.Target.GetPowerAmount<StrengthPower>() > 0) 
            {
                await PowerCmd.Remove<StrengthPower>(cardPlay.Target);
            }
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
        DynamicVars.Damage.UpgradeValueBy(3);
        AddKeyword(CardKeyword.Innate);
    }
}