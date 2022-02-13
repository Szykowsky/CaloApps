export interface MealsFilter {
    dateType: Datetype,
    dayNumber: number | null,
    monthNumber: number | null
}

export enum Datetype {
    None,
    Day,
    Month,
    Both
}