﻿using SightReadingPractice.Domain.Models;
using SightReadingPractice.Domain.Repositories;
using SightReadingPractice.Domain.Entities;
using System.Linq;

namespace SightReadingPractice.Domain.Interactors
{
    public class NoteExerciseInteractor
    {
        private readonly INoteExerciseResultRepository _noteExerciseResultRepository;

        public NoteExerciseInteractor(INoteExerciseResultRepository noteExerciseResultRepository)
        {
            _noteExerciseResultRepository = noteExerciseResultRepository;
        }

        public void AddExerciseResult(ExerciseResultInputModel exerciseResultInputModel)
        {
            var noteExerciseResults = exerciseResultInputModel.ExerciseResult.Select(r => 
            {
                NoteExerciseResult entity = new();

                entity.DateTime = exerciseResultInputModel.DateTime;
                entity.ActualTone = r.Note.Tone;
                entity.SeptimaArea = r.Note.SeptimaArea;
                entity.Success = r.Success;

                return entity;
            });

            _noteExerciseResultRepository.AddNoteExerciseResults(noteExerciseResults);
        }
    }
}
