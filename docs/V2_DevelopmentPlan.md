# PayrollSystem V2.0 Development Plan

## Goal

Upgrade the existing PayrollSystem from a basic console application into a more modular and enterprise-oriented payroll processing system.

The V2.0 upgrade will introduce project separation, generic repositories, advanced collections, custom exceptions, metadata, serialization, streams, asynchronous processing, and improved documentation.

## Phase 1 — Project Restructuring

- Create `PayrollSystem.Core` as a Class Library.
- Create `PayrollSystem.App` as a Console Application.
- Move models, interfaces, and payroll logic into `PayrollSystem.Core`.
- Keep `Program.cs` inside `PayrollSystem.App`.
- Add project references between App and Core.
- Add one NuGet package for serialization or output formatting.

## Phase 2 — Generics and Generic Delegates

- Create `Repository<T> where T : Employee`.
- Add generic methods for storing and retrieving employees.
- Replace the old custom `PayrollHandler` delegate with `Action<string>`.
- Use `Func<Employee, decimal>` for dynamic bonus calculations.
- Use `Func<Employee, decimal>` for dynamic deduction calculations.
- Use `Predicate<Employee>` for employee filtering.

## Phase 3 — Documentation and Custom Exceptions

- Add XML documentation to important classes and methods.
- Use `<summary>`, `<param>`, `<returns>`, and `<exception>`.
- Create `PayrollProcessingException`.
- Replace suitable general exceptions with the custom payroll exception.

## Phase 4 — Advanced Collections

- Use `Dictionary<int, Employee>` for employee lookup by ID.
- Use `Queue<PaymentTransaction>` for pending payments.
- Use `Stack<string>` for audit/undo history.
- Use `HashSet<string>` for unique email or identity validation.
- Use `SortedSet<Employee>` for automatic employee ordering.
- Add `GetHighEarners(decimal threshold)` using `yield return`.

## Phase 5 — Extension Methods, Attributes, and Reflection

- Create `ToCurrencyString()` extension method for `decimal`.
- Create `AuditTrailAttribute`.
- Add audit metadata such as operation name and author.
- Apply the attribute to payroll-related classes or methods.
- Use Reflection to inspect and print audit metadata at startup.

## Phase 6 — Streams and Serialization

- Save payroll or audit records using `FileStream` and `StreamWriter`.
- Export employee data to JSON.
- Import employee data from JSON.
- Integrate serialization into the application workflow.

## Phase 7 — Async and Background Processing

- Convert payroll processing to `ProcessPayrollAsync(...)`.
- Use `async` and `await`.
- Use `Task.Delay(...)` to simulate payment gateway responses.
- Add a background worker using `Task.Run` or a separate thread.
- Process queued payment transactions while user interaction continues.

## Phase 8 — Final Integration and Testing

- Run Reflection inspection at application startup.
- Load employees from JSON or create them manually.
- Validate duplicate emails using `HashSet`.
- Queue at least five payment transactions.
- Process payments asynchronously.
- Filter high earners using `yield return`.
- Save the high-earner report using `StreamWriter`.
- Perform final regression testing of V1 functionality.
- Update architecture and flow diagrams.
- Update README for V2.0.

## Git Strategy

V1 remains stable on the `master` branch.

All V2.0 development will be completed on:

`v2-enterprise-upgrade`

Each major requirement will be implemented using a separate commit similar to V1 implementation.
