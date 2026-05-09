using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class BuJianTiHuan: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(2)];
	public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(MyKeywords.Modification)
    ];
	public BuJianTiHuan(): base(0, CardType.Skill, CardRarity.Rare, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		CardModel item = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt,0,1), context: choiceContext, player: base.Owner, filter:(CardModel card) => card.Keywords.Contains(MyKeywords.Modification), source: this)).FirstOrDefault();
        if (item != null)
		{
			List<CardModel> card = [item.CreateClone(),item.CreateClone()];
            if (cardPlay.Card.IsUpgraded)
			{
				card.Add(item.CreateClone());
			}
		    IReadOnlyList<CardPileAddResult> results = await CardPileCmd.AddGeneratedCardsToCombat(card, PileType.Discard, cardPlay.Card.Owner);
			CardCmd.PreviewCardPileAdd(results);
        }
	}
	protected override void OnUpgrade()
	{
		base.DynamicVars.Cards.UpgradeValueBy(1);
	}
}
