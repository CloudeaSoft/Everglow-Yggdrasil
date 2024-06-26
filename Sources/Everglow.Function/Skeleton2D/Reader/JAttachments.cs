using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Everglow.Commons.Skeleton2D.Reader;

/// <summary>
/// Attachment的实际数据
/// </summary>
public class JAttachmentInner
{
	[JsonIgnore]
	public string ImageName
	{
		get; set;
	}
	/// <summary>
	/// 附件类型:
	/// region: 一个textured矩形.
	/// mesh: 一个textured网格, 其顶点受到多个有权重骨骼的影响.
	/// linkedmesh: 与另一个网格共享的UVs、顶点和权重的网格.
	/// boundingbox: 一个用于撞击检测, 物理运动等功能的多边形.
	/// path: 一个通常用于沿着路径来移动骨骼的三次样条曲线.
	/// point: 一个带有旋转的单点, 通常用于产生弹丸或粒子.
	/// clipping: 一个多边形, 用于在绘制中裁剪其他附件.
	/// </summary>
	[DefaultValue("region")]
	[JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public string Type = "region";

	[JsonProperty(PropertyName = "x", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public float X = 0f;

	[JsonProperty(PropertyName = "y", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public float Y = 0f;

	[JsonProperty(PropertyName = "rotation", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public float Rotation = 0f;

	[JsonProperty(PropertyName = "width", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public float Width = 0f;

	[JsonProperty(PropertyName = "height", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public float Height = 0f;

	/// <summary>
	/// 一个坐标列表, 表示每个顶点的纹理坐标
	/// </summary>
	[JsonProperty(PropertyName = "uvs", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public List<float> UVList = new();

	/// <summary>
	/// 一个顶点索引列表, 定义了网格中的每个三角形
	/// </summary>
	[JsonProperty(PropertyName = "triangles", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public List<int> TriangleIndiciesList = new();


	/// <summary>
	/// 包含每个顶点的x,y对, 影响该顶点的骨骼数量(用于加权网格), 以及用于这些骨骼的: 
	/// 骨骼索引, 绑定位置X坐标, 绑定位置Y坐标, 权重. 若顶点数量 > UV数量, 则该网格为加权网格
	/// </summary>
	[JsonProperty(PropertyName = "vertices", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public List<float> VerticesList = new();

	/// <summary>
	/// 构成多边形壳的顶点数量. 壳顶点保持在vertices列表首位
	/// </summary>
	[JsonProperty(PropertyName = "hull", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public int Hull;

	/// <summary>
	/// 一个顶点索引对的列表, 定义了连接顶点之间的边
	/// </summary>
	[JsonProperty(PropertyName = "edges", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
	public List<int> Edges = new();


}


public class JAttachment
{
	public string Name
	{
		get; set;
	}

	public List<JAttachmentInner> AttachmentInners
	{
		get; set;
	}
}

public class AttachmentsJsonConverter : JsonConverter<JAttachments>
{
	public override JAttachments ReadJson(JsonReader reader, Type objectType, [AllowNull] JAttachments existingValue, bool hasExistingValue, JsonSerializer serializer)
	{
		if (reader.TokenType != JsonToken.StartObject)
			throw new JsonException();
		var obj = JObject.Load(reader);

		var attachments = new JAttachments();
		foreach (var kvPair in obj)
		{
			var attachment = new JAttachment()
			{
				Name = kvPair.Key,
				AttachmentInners = new List<JAttachmentInner>()
			};

			foreach (var attPair in kvPair.Value as JObject)
			{
				var attachment_inner = attPair.Value.ToObject<JAttachmentInner>();
				attachment_inner.ImageName = attPair.Key;
				attachment.AttachmentInners.Add(attachment_inner);
			}
			attachments.Attachments.Add(attachment);
		}
		return attachments;
	}

	public override void WriteJson(JsonWriter writer, [AllowNull] JAttachments value, JsonSerializer serializer)
	{
		return;
	}
}

public class JAttachments
{
	public List<JAttachment> Attachments;

	public JAttachments()
	{
		Attachments = new List<JAttachment>();
	}
}
