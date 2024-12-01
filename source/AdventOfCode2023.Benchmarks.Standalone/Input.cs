using System.Text;

namespace AdventOfCode2023.Benchmarks.Standalone;

public class Input
{
	public string Text { get; }
	public byte[] Bytes { get; }
	public string[] Lines { get; }
	public Memory<string> Memory { get; }
	public IEnumerable<string> Enumerable { get; }

	public Input(string text, string[] lines, IEnumerable<string> enumerable)
	{
		Text = text;
		Lines = lines;
		Enumerable = enumerable;
		Memory = new Memory<string>(lines);
		Bytes = Encoding.UTF8.GetBytes(Text);
	}
}