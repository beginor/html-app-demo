'use strict';

let gulp = require('gulp'),
    sass = require('gulp-sass'),
    gls = require('gulp-live-server'),
    sourcemaps = require('gulp-sourcemaps')
    tsc = require('gulp-typescript');

let root = './',
    ts = [root + 'src/**/*.ts'],
    html = [root + 'src/**/*.html'],
    scss = [root + 'scss/**/*.scss'],
    dist = root + 'build',
    port = 3000;

gulp.task('sass', function () {
    gulp.src(scss)
        .pipe(sass())
        .pipe(gulp.dest(dist))
});

gulp.task('tsc', function () {
    var proj = tsc.createProject('tsconfig.json');
    gulp.src(ts)
        .pipe(sourcemaps.init())
        .pipe(tsc(proj))
        .js
        .pipe(sourcemaps.write(root))
        .pipe(gulp.dest(dist));
});

gulp.task('copy', function () {
    gulp.src(html).pipe(gulp.dest(dist));
});

gulp.task('build', ['sass', 'tsc', 'copy']);

gulp.task('serve', function () {
    var server = gls.static(root, port);
    server.start();
    
    gulp.watch([
        dist + '**/*.js',
        dist + '**/*.css',
        dist + '**/*.html'
    ], function (file) {
        server.notify.apply(server, [file]);
    });
});

gulp.task('dev', ['build', 'serve'], function () {
    gulp.watch(ts, ['tsc']);
    gulp.watch(scss, ['sass']);
    gulp.watch(html, ['copy']);
});

