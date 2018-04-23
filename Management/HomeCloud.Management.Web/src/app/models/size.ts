const SIZE_NAMES: Array<string> = ["B", "Kb", "Mb", "Gb", "Tb"];
const BYTE_DELIMITER: number = 1024;

export class Size {
    public value: number = 0;
    public get text(): string {
        return this.calculate(this.value ? this.value : 0, 0);
    }

    private calculate(value: number, depth: number): string {
        if (value < 800 || depth == SIZE_NAMES.length - 1) {
            return value + " " + SIZE_NAMES[depth];
        }
        else {
            value = this.round(value / BYTE_DELIMITER, 2);

            return this.calculate(value, ++depth);
        }
    }

    private round(value: number, precision: number): number {
        return this.shift(Math.round(this.shift(value, precision, false)), precision, true);
      }

    private shift(value: number, precision: number, reverseShift: boolean): number {
        if (reverseShift) {
          precision = -precision;
        }

        let numArray = ("" + value).split("e");
        return +(numArray[0] + "e" + (numArray[1] ? (+numArray[1] + precision) : precision));
      };
}