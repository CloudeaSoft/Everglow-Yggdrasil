using Everglow.Myth;

namespace Everglow.Myth.Misc.Items.Weapons.Clubs;

public class CurseClub : ClubItem
{
	public override void SetStaticDefaults()
	{
		
	}

	public override void SetDef()
	{
		
		Item.damage = 57;
		Item.value = 5000;
		ProjType = ModContent.ProjectileType<Projectiles.Weapon.Melee.Clubs.CurseClub>();
	}
}