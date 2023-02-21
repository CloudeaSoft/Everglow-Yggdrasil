namespace Everglow.Core.Network.PacketHandle
{
	/// <summary>
	/// ���ڱ�ʾһ������Ķ�д�����Լ������ݣ�ע��һ������������ֻ�������ݣ��������պ�Ĳ���
	/// </summary>
	public interface IPacket
	{
		/// <summary>
		/// �������ʱд�����ݵ��߼����֣�ע���ʱд������ݿ�ͷ�Ѿ������˷��ID�����Բ����ظ�д��
		/// </summary>
		/// <param name="writer"></param>
		public void Send(BinaryWriter writer);

		/// <summary>
		/// �����ȡʱ�������ݵ��߼����֣�ע���ʱ�Ѿ������˷��ID�����Բ�Ҫ�ظ���ȡ
		/// </summary>
		/// <param name="reader"></param>
		/// <param name="whoAmI">the ID of whomever sent the packet (equivalent to the Main.myPlayer of the sender)</param>
		public void Receive(BinaryReader reader, int whoAmI);
	}
}
