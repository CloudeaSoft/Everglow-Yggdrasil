namespace Everglow.Commons.Utilities;

public static class ProjectileUtils
{
	public abstract class StickNPCProjectile : ModProjectile
	{
		/// <summary>
		/// Ŀ�����,Ŀ�����������ʱ���ĳ�-2
		/// </summary>
		public int StuckNPC = -1;
		/// <summary>
		/// ��ԽǶ�
		/// </summary>
		public float RelativeAngle = 0;
		/// <summary>
		/// ����ʱ�ĽǶ�
		/// </summary>
		public float HitTargetAngle = 0;
		/// <summary>
		/// ���λ��
		/// </summary>
		public Vector2 RelativePos = Vector2.zeroVector;
		/// <summary>
		/// ����ʱ�ֵĴ�С
		/// </summary>
		public float HitTargetScale = 1f;
		public override void AI()
		{
			UpdateSticking();
		}
		public virtual void UpdateSticking()
		{
			if (StuckNPC >= 0 && StuckNPC < Main.maxNPCs)
			{
				NPC target = Main.npc[StuckNPC];
				if (target == null || !target.active)
				{
					StuckNPC = -2;
					return;
				}
				else
				{
					Projectile.rotation = target.rotation + RelativeAngle;
					float scaleRate = target.scale / HitTargetScale;
					Projectile.Center = target.Center + RelativePos.RotatedBy(target.rotation + RelativeAngle - HitTargetAngle) * scaleRate;
					Projectile.velocity = target.velocity;
				}
			}
		}
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			RelativeAngle = Projectile.rotation - target.rotation;
			HitTargetAngle = Projectile.rotation;
			RelativePos = Projectile.Center - target.Center;
			HitTargetScale = target.scale;
			StuckNPC = target.whoAmI;
		}
	}
}
