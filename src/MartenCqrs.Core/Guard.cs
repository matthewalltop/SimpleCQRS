namespace MartenCqrs.Core;

public interface IGuardClause {
}

public class Guard: IGuardClause {
	public static IGuardClause Against => new Guard();
	private Guard() { }
}