import { TestBed } from '@angular/core/testing';

import { MealApiService } from './meal-api.service';

describe('MealApiService', () => {
    let service: MealApiService;

    beforeEach(() => {
        TestBed.configureTestingModule({});
        service = TestBed.inject(MealApiService);
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });
});
