import { TestBed, ComponentFixture, async } from '@angular/core/testing';
import { PermissionComponent } from './permission.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ToastrModule } from 'ngx-toastr';
import { NgxsModule, Actions, ofActionDispatched } from '@ngxs/store';
import { PermissionState } from '../permission/permission.state';
import { RoleState } from '../role/role.state';
import { UserState } from '../user/user.state';
import { RoleService } from '../shared/service/role.service';
import { UserService } from '../shared/service/user.service';
import { PermissionService } from '../shared/service/permission.service';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Store, State } from '@ngxs/store';
import { Observable } from 'rxjs';
import { GetPermissions } from './permission.action';


describe('PermissionComponent', () => {
    let component: PermissionComponent;
    let fixture: ComponentFixture<PermissionComponent>;
    let store : Store;
    let spy: jasmine.Spy;
    let actions$: Observable<any>;

    beforeEach(async() => {
        TestBed.configureTestingModule({
            declarations: [PermissionComponent],
            imports: [    MatToolbarModule,
                MatButtonModule,
                MatTableModule,
                MatPaginatorModule,
                MatDialogModule,
                MatInputModule,
                BrowserAnimationsModule,
                MatSelectModule,
                HttpClientModule,
                NgxsModule.forRoot([PermissionState, RoleState, UserState]),
                ToastrModule.forRoot({
                    timeOut: 2000,
                    positionClass: 'toast-bottom-right',
                    preventDuplicates: false
                  })],
                  providers: [
                    UserService,
                    PermissionService,
                    RoleService
                  ]
        }).compileComponents();
    });

    beforeEach(()=>{
        fixture = TestBed.createComponent(PermissionComponent);
        component = fixture.componentInstance;
    })

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});