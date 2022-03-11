import {
    ChangeDetectionStrategy,
    Component,
    EventEmitter,
    Input,
    Output,
} from '@angular/core';
import { FormGroup } from '@angular/forms';
import { AddMealModel } from '../../models/add-meal-model';

@Component({
    selector: 'calo-add-meal-form',
    templateUrl: './add-meal-form.component.html',
    styleUrls: ['./add-meal-form.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddMealFormComponent {
    @Input() addMealForm: FormGroup | undefined;
    @Input() diets: { [key: string]: string; } | null | undefined
    @Output() submitForm: EventEmitter<AddMealModel> = new EventEmitter();

    handleAddMeal() {
        const addMealModel: AddMealModel = { dietId: this.addMealForm?.controls['diet']?.value, ...this.addMealForm?.value };
        this.submitForm.emit(addMealModel);
    }
}
