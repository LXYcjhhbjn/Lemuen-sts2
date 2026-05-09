using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class ChangeGun: CustomCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AMMO>(1m),new CardsVar(1)];
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<AMMO>(),
        HoverTipFactory.FromKeyword(MyKeywords.Guns)
    ];	
	public ChangeGun()
		: base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self,true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CardPileCmd.Draw(choiceContext, base.DynamicVars.Cards.BaseValue, base.Owner);
		await PowerCmd.Apply<AMMO>(choiceContext , base.Owner.Creature, base.DynamicVars["AMMO"].BaseValue, base.Owner.Creature, this);
        List<CardModel> list = PileType.Hand.GetPile(base.Owner).Cards.Where((CardModel c) => c != null && c.Type == CardType.Attack).ToList();
		int cardCount = list.Count;
		foreach (CardModel item in list)
		{
			await CardCmd.Exhaust(choiceContext, item);
		}
        int num= PileType.Exhaust.GetPile(base.Owner).Cards.Where((CardModel c) => c.Keywords.Contains(MyKeywords.Guns)).ToList().Count;
		if (num > 0)
		{
			await CardPileCmd.Add(await CardSelectCmd.FromSimpleGrid(choiceContext, (from c in PileType.Exhaust.GetPile(base.Owner).Cards
				where c.Keywords.Contains(MyKeywords.Guns)
				select c).ToList(), base.Owner, new CardSelectorPrefs(base.SelectionScreenPrompt, 0 , 1)), PileType.Hand);
		}
	}
	protected override void OnUpgrade()
	{
        base.DynamicVars["AMMO"].UpgradeValueBy(1m);
	}
}