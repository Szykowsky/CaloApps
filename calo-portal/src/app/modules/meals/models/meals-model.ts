import { PaginationBase } from "src/app/shared/models/pagination-base";

export interface Meal {
    id: string;
    date: Date;
    name: string;
    kcal: number;
}

export interface MealsQueryResult {
    queryResult: Meal[],
    paginationBase: PaginationBase | null
}