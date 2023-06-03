namespace Sandbox.Common.Worlds
{

  public class InteractionResult
  {
    public int Result;

    public static InteractionResult SUCCESS = new InteractionResult() { Result = 0 };
    public static InteractionResult CONSUME = new InteractionResult() { Result = 1 };
    public static InteractionResult CONSUME_PARTIAL = new InteractionResult() { Result = 2 };
    public static InteractionResult PASS = new InteractionResult() { Result = 4 };
    public static InteractionResult FAIL = new InteractionResult() { Result = 8 };


    public bool ConsumesAction()
    {
      return this == InteractionResult.SUCCESS || this == InteractionResult.CONSUME || this == InteractionResult.CONSUME_PARTIAL;
    }

    public bool ShouldSwing()
    {
      return this == InteractionResult.SUCCESS;
    }

    public bool ShouldAwardStats()
    {
      return this == InteractionResult.SUCCESS || this == InteractionResult.CONSUME;
    }

    public static InteractionResult SidedSuccess(bool cond)
    {
      return cond ? InteractionResult.SUCCESS : InteractionResult.CONSUME;
    }
  }

}