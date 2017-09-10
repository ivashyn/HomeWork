"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var core_1 = require("@angular/core");
var task_1 = require("./task");
var mock_task_1 = require("./mock-task");
var TaskService = (function () {
    function TaskService() {
    }
    TaskService.prototype.getTasks = function () {
        return new Promise(function (resolver) {
            setTimeout(function () {
                resolver(mock_task_1.TASKS);
            }, 500);
        });
    };
    TaskService.prototype.getTask = function (id) {
        return new Promise(function (resolver) {
            var task = mock_task_1.TASKS.find(function (f) { return f.id === id; });
            setTimeout(function () {
                resolver(task);
            }, 500);
        });
    };
    TaskService.prototype.addTask = function (taskText) {
        var task = new task_1.Task();
        var farafa = mock_task_1.TASKS[mock_task_1.TASKS.length - 1]; //bad
        var idToAdd = farafa.id + 1;
        task.text = taskText;
        task.isCompleted = false;
        task.id = idToAdd;
        mock_task_1.TASKS.push(task);
    };
    TaskService.prototype.removeTask = function (id) {
        var task = this.getTask(id);
        var index = mock_task_1.TASKS.findIndex(function (t) { return t.id == id; });
        mock_task_1.TASKS.splice(index, 1);
    };
    TaskService.prototype.completeTask = function (task) {
        var task = mock_task_1.TASKS.find(function (f) { return f.id === task.id; });
        task.isCompleted = !task.isCompleted;
    };
    return TaskService;
}());
TaskService = __decorate([
    core_1.Injectable()
], TaskService);
exports.TaskService = TaskService;
//# sourceMappingURL=task.service.js.map