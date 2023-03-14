using Everglow.Myth;

namespace Everglow.Myth.MiscItems.Weapons.Clubs;

public class MeteorClub : ClubItem
{
	public override void SetStaticDefaults()
	{
		
	}

	public override void SetDef()
	{
		
		Item.damage = 16;
		Item.value = 576;
		ProjType = ModContent.ProjectileType<Projectiles.MeteorClub>();
	}
	public override void AddRecipes()
	{
		CreateRecipe()
			.AddIngredient(ItemID.MeteoriteBar, 18)
			.AddTile(TileID.WorkBenches)
			.Register();
	}
}
