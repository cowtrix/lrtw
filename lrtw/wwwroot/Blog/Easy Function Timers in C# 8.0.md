2020-12-07 #programming

A function timer is a class that can monitor the execution of functions and time them. If you’ve got some upper limit on how long a function should take then these timers can be very useful in detecting drops in performance.

With C# 8.0, you get some nice features that allow for a nice little function timer class, namely:

Inline using statements, which let you declare IDisposable objects in a using statement without using braces and just use the scope of the declaring function. This is absolutely perfect for a function timer! We can inline declare an object that will be cleaned up when the function leaves scope (with 1 or 2 tricky edge cases, of course…)

Discards, introduced in C# 7.0, also mean we don’t have to bring any local variables into play when we create this using variable! This is great because it means the function actually can’t mess around with its timer object, while still being able to use the scope.

There’s a few other nice little tools we can use – CallerMemberNameAttribute lets us grab the calling method name in a performant way.

So, check this out:

```
public class FunctionTimer : IDisposable
{
	private string m_memberName;
	private TimeSpan m_maxTime;
	private Stopwatch m_stopwatch;
	private bool m_isDisposed;

	public FunctionTimer(TimeSpan maxTime, [CallerMemberName] string memberName = "")
	{
		m_maxTime = maxTime;
		m_stopwatch = Stopwatch.StartNew();
		m_memberName = memberName;
	}

	public void Dispose()
	{
		if (m_isDisposed)
		{
			return;
		}
		m_isDisposed = true;
		if (m_stopwatch.Elapsed > m_maxTime)
		{
			Console.WriteLine($"Warning: Long running function: {m_memberName}. {m_stopwatch.Elapsed} - expected max {m_maxTime}");
			// Debugger.Break();
 - can even break the debugger here if you'd like for further investigation
		}
	}
}
```

So, how do you use it?

```
void MyMaybeLongRunningFunction()
{
    using var _ = new FunctionTimer(TimeSpan.FromSeconds(2));
    /* . . .
    Do some things which need to happen within a certain timeframe
    . . . */
}  // When the callstack collapses, the function timer will dispose and the timer will get checked
```

You can even use it with brackets to time portions of a method:

```
void MyMaybeLongRunningFunction()
{
    using (var _ = new FunctionTimer(TimeSpan.FromSeconds(1)))
    {
        /* do the thing */
    } // timer gets checked here
}
```

Note that there are some weird things that happen when you depend on scope cleanup for function timing, especially around try/catch/finally blocks. For instance:

```
void MyFunction()
{
	using var _ = new FunctionTimer(TimeSpan.FromSeconds(2));
	try
	{
		// do a thing that maybe throws an exception
	}
	catch (Exception e)	// timer stops as we enter the catch
	{
		// So exception cleanup time isn't counted
	}
	finally
	{
		// We might then do a finally block that maybe takes some time - but this won't be counted either!
	}
	// But note - this only happens if the method ends here! Optimisation I guess?
}
```

Interesting stuff! Now go time your functions easier than ever 🙂