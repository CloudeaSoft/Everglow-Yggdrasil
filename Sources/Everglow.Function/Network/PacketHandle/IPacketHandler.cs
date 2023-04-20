namespace Everglow.Commons.Network.PacketHandle;

/// <summary>
/// ���պʹ���ĳ�����͵ķ�����߼�
/// </summary>
public interface IPacketHandler
{
	/// <summary>
	/// ���յ�����Ժ��ʵ���߼�����
	/// </summary>
	/// <param name="packet"></param>
	/// <param name="whoAmI"></param>
	public void Handle(IPacket packet, int whoAmI);
}

/// <summary>
/// ����ָ��һ��IPacketHandler��Ҫ�����IPacket���ͣ������϶�һ��Handlerֻ����һ�ַ��
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class HandlePacketAttribute : Attribute
{
	private Type packetType;

	public HandlePacketAttribute(Type type)
	{
		packetType = type;
		Debug.Assert(typeof(IPacket).IsAssignableFrom(type));
	}

	public Type PacketType
	{
		get
		{
			return packetType;
		}
	}
}
