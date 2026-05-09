using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace LXY.Scripts;

[Pool(typeof(TokenCardPool))]
public class MaZuiZhuangTian: CustomCardModel
{
    public override string PortraitPath => $"res://lemuen/images/cards/{Id.Entry.ToLowerInvariant()}.png";
	public MaZuiZhuangTian() : base(-1, CardType.Skill, CardRarity.Rare, TargetType.None){}
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<MaZuiDan>(choiceContext , base.Owner.Creature, 5, base.Owner.Creature, this);
    }
}