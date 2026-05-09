using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(TokenCardPool))]
public class ZhiYuanBaoLieLiMing: CustomCardModel
{
	protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(6)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust,CardKeyword.Ethereal];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromCard<BaoLieLiMing>()
	];
    public ZhiYuanBaoLieLiMing() : base(3, CardType.Skill, CardRarity.Rare, TargetType.Self,true)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
		List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.ToList();
		int cardCount = list.Count;
		foreach (CardModel item in list)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
		int num= 6;
		List<CardModel> list1 = new List<CardModel>();
		for (int i = 0; i < num; i++)
		{
			list1.Add(base.CombatState.CreateCard<BaoLieLiMing>(base.Owner));
		}
		await CardPileCmd.AddGeneratedCardsToCombat(list1, PileType.Hand, cardPlay.Card.Owner);
    }
	protected override void OnUpgrade()
	{
        AddKeyword(CardKeyword.Innate);
	}
}
