var gulp = require('gulp'),
    sass = require('gulp-sass'),
    gls = require('gulp-live-server'),
    sourcemaps = require('gulp-sourcemaps')
    ts = require('gulp-typescript');

var src = ['src/**/*.ts'],
    html = ['src/**/*.html'],
    scss = ['scss/**/*.scss'],
    build = 'build',
    servePort = 3000;

gulp.task('sass', function () {
    'use strict';
    gulp.src(scss)
        .pipe(sass())
        .pipe(gulp.dest('build'))
});

gulp.task('tsc', function () {
    'use strict';
    var proj = ts.createProject('tsconfig.json');
    gulp.src(src)
        .pipe(sourcemaps.init())
        .pipe(ts(proj))
        .js
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest(build));
});

gulp.task('copy', function () {
    'use strict';
    gulp.src(html)
        .pipe(gulp.dest(build));
});

gulp.task('build', ['sass', 'tsc', 'copy']);

gulp.task('serve', function () {
    'use strict';
    var server = gls.static('./', servePort);
    server.start();
    
    gulp.watch([
        build + '**/*.js',
        build + '**/*.css',
        build + '**/*.html'
    ], function (file) {
        server.notify.apply(server, [file]);
    });
});

gulp.task('dev', ['build', 'serve'], function () {
    gulp.watch(src, ['tsc']);
    gulp.watch(scss, ['sass']);
    gulp.watch(html, ['copy']);
});

