import { TestBed } from '@angular/core/testing';
import { UserCreateDialogComponent } from './user-create-dialog.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ToastrModule } from 'ngx-toastr';
import { NgxsModule } from '@ngxs/store';
import { UserState } from '../user/user.state';
import { UserService } from '../shared/service/user.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

describe('UserCreateDialogComponent', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [UserCreateDialogComponent],
            imports: [    MatToolbarModule,
                MatButtonModule,
                MatTableModule,
                MatPaginatorModule,
                MatDialogModule,
                MatInputModule,
                MatSelectModule,
                HttpClientModule,
                FormsModule, 
                ReactiveFormsModule,
                NgxsModule.forRoot([UserState]),
                ToastrModule.forRoot({
                    timeOut: 2000,
                    positionClass: 'toast-bottom-right',
                    preventDuplicates: false
                  })],
                  providers: [
                    UserService,
                    { provide: MatDialogRef, useValue: {} },
                    { provide: MAT_DIALOG_DATA, useValue: {} }
                  ],
        });
    });

    it('should create component instance', () => {
        const fixture = TestBed.createComponent(UserCreateDialogComponent);
        const component = fixture.debugElement.componentInstance;
        expect(component).toBeTruthy();
    });
});