using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class AnHunQuPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool AllowNegative => false;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/AnHunQuPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/AnHunQuPower.png";
    private HashSet<CardModel>? _autoplayingCards;
	private HashSet<CardModel> AutoplayingCards
	{
		get
		{
			AssertMutable();
			if (_autoplayingCards == null)
			{
				_autoplayingCards = new HashSet<CardModel>();
			}
			return _autoplayingCards;
		}
	}
	public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
	{
		if (player == base.Owner.Player)
		{
			Flash();
		    foreach (CardModel item in await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 0, base.Amount), context: choiceContext, player:player, filter: null, source: this))
			{
		    	if (item != null)
		    	{
			    	if (item.IsUpgradable) {CardCmd.Upgrade(item);}
					if (item.CanPlay())
					{
			    		AutoplayingCards.Add(item);
			    		await CardCmd.AutoPlay(choiceContext,item, null);
			    		AutoplayingCards.Remove(item);
					}
		    	}
			}
		}
	}
}
