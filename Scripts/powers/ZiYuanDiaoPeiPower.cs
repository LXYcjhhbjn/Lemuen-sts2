using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
namespace LXY.Scripts;

public class ZiYuanDiaoPeiPower: CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    public override string? CustomPackedIconPath => "res://lemuen/images/powers/ZiYuanDiaoPeiPower.png";
    public override string? CustomBigIconPath => "res://lemuen/images/powers/ZiYuanDiaoPeiPower.png";
	public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState icombatState)
	{
		if (player != base.Owner.Player)
		{
			return;
		}
		int num=PileType.Exhaust.GetPile(player).Cards.Where((CardModel c) => c.Keywords.Contains(MyKeywords.Modification)).ToList().Count();
		if (num <= 4)
		{
			await CardPileCmd.Add(PileType.Exhaust.GetPile(player).Cards.Where((CardModel c) => c.Keywords.Contains(MyKeywords.Modification)).ToList(), PileType.Hand);
		}
		else
		{
			await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, (from c in PileType.Exhaust.GetPile(base.Owner.Player).Cards
				where c.Keywords.Contains(MyKeywords.Modification)
				select c).ToList(), player, new CardSelectorPrefs(base.SelectionScreenPrompt, 0 , 4)), PileType.Hand);
		}
	}
}