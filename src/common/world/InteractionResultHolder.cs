namespace Sandbox.Common.Worlds
{
  public class InteractionResultHolder<T>
  {
    public InteractionResult result { get; private set; }
    public T obj { get; private set; }

    public InteractionResultHolder(InteractionResult result, T obj)
    {
      this.result = result;
      this.obj = obj;
    }

    public static InteractionResultHolder<T> Success(T obj)
    {
      return new InteractionResultHolder<T>(InteractionResult.SUCCESS, obj);
    }

    public static InteractionResultHolder<T> Consume(T obj)
    {
      return new InteractionResultHolder<T>(InteractionResult.CONSUME, obj);
    }

    public static InteractionResultHolder<T> Pass(T obj)
    {
      return new InteractionResultHolder<T>(InteractionResult.PASS, obj);
    }

    public static InteractionResultHolder<T> Fail(T obj)
    {
      return new InteractionResultHolder<T>(InteractionResult.FAIL, obj);
    }

    public static InteractionResultHolder<T> SidedSuccess(T obj, bool cond)
    {
      return cond ? Success(obj) : Consume(obj);
    }
  }

}