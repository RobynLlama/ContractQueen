# Task Syncing

As of writing this guide I am uncertain if tasks that are bound to methods only run on the Server will actually sync to clients. For example using a hook on ServerAddToInventory will only run and update the runtimeTask on the host and I do not know if that will be sync'd with clients.

> 👑 The Queen's Advice
>
> While more testing and dissecting game code *IS* required here, I can say with certainty that no existing game task is tied to a server exclusive task and, therefor, recommend that you follow this best-practice as though it were true until demonstrated otherwise.
