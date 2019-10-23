import { TestBed } from '@angular/core/testing';
import { PermissionDeleteDialogComponent } from './permission-delete-dialog.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ToastrModule } from 'ngx-toastr';
import { NgxsModule } from '@ngxs/store';
import { PermissionState } from '../permission/permission.state';
import { PermissionService } from '../shared/service/permission.service';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

describe('PermissionDeleteDialogComponent', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [PermissionDeleteDialogComponent],
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
                NgxsModule.forRoot([PermissionState]),
                ToastrModule.forRoot({
                    timeOut: 2000,
                    positionClass: 'toast-bottom-right',
                    preventDuplicates: false
                  })],
                  providers: [
                    PermissionService,
                    { provide: MatDialogRef, useValue: {} },
                    { provide: MAT_DIALOG_DATA, useValue: {} }
                  ],
        });
    });

    it('should create component instance', () => {
        const fixture = TestBed.createComponent(PermissionDeleteDialogComponent);
        const component = fixture.debugElement.componentInstance;
        expect(component).toBeTruthy();
    });
});