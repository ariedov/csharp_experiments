# CSharp Experiments

While working with C# recently without having a decent experience in it, I am sometimes wondering how one or another case is working in the language.

Without having the ability to alter any production project just for the sake of science, I created this project for experimenting. Feel free to contribute or fix any issues you find valuable.

## Experiment 1. Throw an exception in a non-awaited method.

You can find the experiment details in `AwaitException.Project` and `AwaitException.Test`.

Turns out if just run an `async` method without `await` and an exception occurs in the method - it gets lost in the void.

## Experiment 2. Throw an exception and then rethrow it.

The question is: how the stack trace is affected if we throw an exception, catch it for logging and then rethrow.

The answer: the rethrown exception is having its own stack trace and knows nothing about the previous one.