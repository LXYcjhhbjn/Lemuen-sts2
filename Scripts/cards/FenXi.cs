using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class FenXi: CustomCardModel
{
    protected override bool ShouldGlowGoldInternal => IsPlayable;
    protected override bool IsPlayable => Owner.Creature.HasPower<AMMO>();
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(1)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [MyKeywords.Guns];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>()
    ];
    public FenXi() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self,true)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c.IsUpgradable).ToList();
        if (cardPlay.Card.Owner.Creature.HasPower<AMMO>() && list != null)
		{
			await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature, -1 , base.Owner.Creature, this);
            foreach (CardModel item in await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 0, base.DynamicVars.Cards.IntValue), context: choiceContext, player: cardPlay.Card.Owner.Creature.Player, filter:(CardModel c) => c.IsUpgradable, source: this))
			{
		    	if (item != null)
		    	{
			    	if (item.IsUpgradable) 
                    {
                        CardCmd.Upgrade(item);
                    }
		    	}
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
        base.DynamicVars.Cards.UpgradeValueBy(1);
    }
}