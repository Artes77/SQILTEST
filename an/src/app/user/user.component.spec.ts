import { TestBed } from '@angular/core/testing';
import { UserComponent } from './user.component';
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
import { RoleState } from '../role/role.state';
import { PermissionState } from '../permission/permission.state';
import { RoleService } from '../shared/service/role.service';
import { UserService } from '../shared/service/user.service';
import { PermissionService } from '../shared/service/permission.service';
import { HttpClientModule } from '@angular/common/http';


describe('UserComponent', () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            declarations: [UserComponent],
            imports: [    MatToolbarModule,
                MatButtonModule,
                MatTableModule,
                MatPaginatorModule,
                MatDialogModule,
                MatInputModule,
                MatSelectModule,
                HttpClientModule,
                NgxsModule.forRoot([UserState, RoleState, PermissionState]),
                ToastrModule.forRoot({
                    timeOut: 2000,
                    positionClass: 'toast-bottom-right',
                    preventDuplicates: false
                  })],
                  providers: [
                    UserService,
                    PermissionService,
                    RoleService
                  ],
        });
    });

    it('should create component instance', () => {
        const fixture = TestBed.createComponent(UserComponent);
        const component = fixture.debugElement.componentInstance;
        expect(component).toBeTruthy();
    });
});