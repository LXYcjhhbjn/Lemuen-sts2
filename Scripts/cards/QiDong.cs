using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;

namespace LXY.Scripts;

[Pool(typeof(lemuenCardPool))]
public class QiDong: CustomCardModel
{
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromKeyword(MyKeywords.Modification)];	
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];
	public QiDong() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self , true)
	{
    }
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        List<CardModel> list = PileType.Exhaust.GetPile(base.Owner).Cards.Where((CardModel c) => c.Keywords.Contains(MyKeywords.Modification)).ToList();
		foreach (CardModel item in list)
		{
			await CardPileCmd.Add( item , PileType.Discard);
        }
	}
	protected override void OnUpgrade()
	{
		base.EnergyCost.UpgradeBy(-1);
	}
}
