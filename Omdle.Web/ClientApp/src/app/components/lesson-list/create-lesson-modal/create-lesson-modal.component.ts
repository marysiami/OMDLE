import { Component, Optional, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { CourseService } from "../../../services/course.service";
import { ActivatedRoute } from "@angular/router";
import { DatePipe } from '@angular/common';


@Component({
  selector: "app-create-lesson-modal",
  templateUrl: "./create-lesson-modal.component.html",
  providers: [DatePipe]
})
export class CreateLessonModalComponent {
  newPostForm: FormGroup;
  message: string;
  ownerId: string;
  myDate = new Date();
  date: string;
  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) public data: string,
    public dialogRef: MatDialogRef<CreateLessonModalComponent>,
    private datePipe: DatePipe,
    private courseService: CourseService,
    private formBuilder: FormBuilder) {
    this.date = this.datePipe.transform(this.myDate, 'yyyy-MM-dd');
    this.newPostForm = this.formBuilder.group({
      title: [
        "",
        [Validators.required,Validators.minLength(1)]
      ],
      content: [
        "",
        [Validators.required, Validators.minLength(1)]
      ]      
    });

  }

  submit() {    
    this.courseService
      .createLesson(this.newPostForm.controls.title.value, this.newPostForm.controls.content.value, this.date ,this.data)
      .subscribe(result => {
          this.dialogRef.close();
        },
        error => this.message = "Wystąpił błąd");
  }
}
