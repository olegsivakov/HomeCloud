interface ICommand<T> {
    execute(action: (value: T) => void): void;
}