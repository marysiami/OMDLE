//Modules 
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from "@angular/http";
import { MatDatepickerModule, MatNativeDateModule } from "@angular/material";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Router } from "@angular/router";
import { MaterialModule } from './material.module';
import { SocialLoginModule, AuthServiceConfig, FacebookLoginProvider } from "angularx-social-login";
import { JwtUtil } from './utils/jwt.util';
//Services
import { AuthService } from './services/auth.service';
import { CourseService } from './services/course.service';
import { AdminService } from './services/admin.service';
import { LoggedGuardService } from './utils/LoggedGuard.service';
import { ProfileService } from "./services/profile.service";
import { ChatService } from "./services/chat.service";
//Components
import { AppComponent } from './app.component';
import { CourseListComponent } from './components/course-list/course-list.component';
import { CreateCourseModalComponent } from './components/course-list/create-course-modal/create-course-modal.component';
import { EditCourseModalComponent } from './components/course-list/edit-course-modal/edit-course-modal.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { RegisterComponent } from './components/register/register.component';
import { ChatComponent } from "./components/chat/chat.component";
import { TeacherCourseListComponent } from './components/course-list/teacher-course-list/teacher-course-list.component';
import { UserListComponent } from './components/users/user-list.component';
import { StudentListComponent } from './components/users/students/student-list.component';
import { TeacherListComponent } from './components/users/teachers/teacher-list.component';
import { StudentCourseListComponent } from "./components/course-list/student-course-list/student-course-list.component";
import { ProfileComponent } from './components/profile/profile.component'
import Profileservice = require("./services/profile.service");
import { LessonListComponent } from './components/lesson-list/lesson-list.component';
import { CreateLessonModalComponent } from './components/lesson-list/create-lesson-modal/create-lesson-modal.component';
import { LessonComponent } from './components/lesson/lesson.component';


let config = new AuthServiceConfig([
  {
    provider: new FacebookLoginProvider("2214231655365084"),
    id: FacebookLoginProvider.PROVIDER_ID
  }
]);


export function provideConfig() {
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ChatComponent,
    CourseListComponent,
    CreateCourseModalComponent,
    EditCourseModalComponent,
    TeacherCourseListComponent,
    StudentCourseListComponent,
    UserListComponent,
    StudentListComponent,
    TeacherListComponent,
    ProfileComponent,
    LessonListComponent,
    CreateLessonModalComponent,
    LessonComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: "login", component: LoginComponent },
      { path: "register", component: RegisterComponent },
      { path: "courses/:id", component: CourseListComponent, canActivate: [LoggedGuardService] },
      { path: "teachercourses/:id", component: TeacherCourseListComponent, canActivate: [LoggedGuardService] },
      { path: "studentcourses/:id", component: StudentCourseListComponent },
      { path: "teachers", component: TeacherListComponent, canActivate: [LoggedGuardService] },
      { path: "students", component: StudentListComponent, canActivate: [LoggedGuardService] },
      { path: "profile", component: ProfileComponent },
      { path: "courselessons/:courseId", component: LessonListComponent },
      { path: "lesson/:lessonId", component: LessonComponent }

    ]),
    HttpClientModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    MatDatepickerModule,
    MatNativeDateModule,
    SocialLoginModule
  ],
  providers: [
    AuthService,
    JwtUtil,
    CourseService,
    AdminService,
    LoggedGuardService,
    ChatService,
    ProfileService,
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} },
    { provide: AuthServiceConfig, useFactory: provideConfig },
    { provide: MatDialogRef, useValue: {} }
    // { provide: 'LoggedGuardService', useValue: (authService: AuthService, router: Router) => true }
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    CreateCourseModalComponent,
    EditCourseModalComponent,
    CreateLessonModalComponent
  ]
})
export class AppModule { }
