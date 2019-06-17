import { Component, Optional, Inject } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { CourseService } from "../../../services/course.service";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-create-course-modal",
  templateUrl: "./create-course-modal.component.html"
})
export class CreateCourseModalComponent {
  newPostForm: FormGroup;
  message: string;
  ownerId: string;  

  constructor(
    @Optional() @Inject(MAT_DIALOG_DATA) public data: string,
    public dialogRef: MatDialogRef<CreateCourseModalComponent>,
    private courseService: CourseService,
    private formBuilder: FormBuilder) {
    this.newPostForm = this.formBuilder.group({
      title: [
        "",
        [Validators.required, Validators.pattern(/^[a-zA-Z0-9_.-]*$/),Validators.minLength(1)]
      ]      
    });
  }

  submit() {
    
    this.courseService
      .createCourse(this.newPostForm.controls.title.value,this.data)
      .subscribe(result => {
          this.dialogRef.close();
        },
        error => this.message = "Wystąpił błąd");
  }
}
