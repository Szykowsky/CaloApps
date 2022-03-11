import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'calo-register-form',
    templateUrl: './register-form.component.html',
    styleUrls: ['./register-form.component.scss'],
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterFormComponent implements OnInit {
    public registerForm: FormGroup;
    
    constructor(private formBuilder: FormBuilder) {
        this.registerForm = this.formBuilder.group({
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required]],
            repeatedPassword: ['', Validators.required],
        });
    }

    ngOnInit(): void {}

    handleRegister() {
        console.log('handle register');
    }
}
