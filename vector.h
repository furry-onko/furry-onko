#include <stdlib.h>
#include <string.h>
#include <stdbool.h>
#include <stdint.h>

typedef struct {
	void* data;
	size_t item_count;
	size_t capacity;
	size_t item_size;
} vec_t;


bool vec_new(vec_t* vector, size_t capacity, size_t item_size) {
	if (vector == NULL)
		return false;

	if (item_size == 0)
		return false;

	vector->data = NULL;
	vector->item_count = 0;
	vector->item_size = item_size;
	vector->capacity = capacity;

	if (capacity == 0)
		return true;

	if (capacity > SIZE_MAX / item_size)
		return false;

	void* data = malloc(capacity * item_size);
	if (data == NULL)
		return false;

	vector->data = data;
	vector->capacity = capacity;

	return true;
}

void vec_drop(vec_t* vector) {
	if (vector == NULL)
		return;

	free(vector->data);
	vector->data = NULL;
	vector->capacity = 0;
	vector->item_count = 0;
	vector->item_size = 0;
}

bool vec_push(vec_t* vector, const void* item) {
	if (vector == NULL ||
		item == NULL
	)
		return false;

	if (vector->item_count == vector->capacity) {
		size_t new_capacity = vector->capacity ? vector->capacity * 2 : 1;

		if (new_capacity < vector->capacity)
			return false;

		if (new_capacity > SIZE_MAX / vector->item_size)
			return false;

		void* temp = realloc(vector->data, new_capacity * vector->item_size);
		if (temp == NULL)
			return false;

		vector->data = temp;
		vector->capacity = new_capacity;
	}

	memcpy(
		(unsigned char*)vector->data + vector->item_count * vector->item_size,
		item,
		vector->item_size
	);

	vector->item_count++;
	return true;
}

bool vec_get(const vec_t* vector, size_t index, void* result) {
	if (vector == NULL || result == NULL)
		return false;

	if (index >= vector->item_count)
		return false;

	const unsigned char* data = vector->data;
	memcpy(
		result,
		data + index * vector->item_size,
		vector->item_size
	);

	return true;
}

const void* vec_at(const vec_t* vector, size_t index) {
	if (vector == NULL)
		return NULL;

	if (index >= vector->item_count)
		return NULL;

	return (const unsigned char*)vector->data + index * vector->item_size;
}

bool vec_pop(vec_t* vector, void* out) {
	if (vector == NULL)
		return false;

	if (vector->item_count == 0)
		return false;

	if (out != NULL) {
		const unsigned char* data = vector->data;
		memcpy(
			out,
			data + (vector->item_count-1)*vector->item_size,
			vector->item_size
		);
	}

	vector->item_count--;
	return true;
}

bool vec_shrink(vec_t* vector) {
	if (vector == NULL)
		return false;

	if (vector->capacity == vector->item_count)
		return true;

	if (vector->item_count == 0) {
		free(vector->data);
		vector->data = NULL;
		vector->capacity = 0;
		return true;
	}

	if (vector->item_count > SIZE_MAX / vector->item_size)
		return false;

	void* temp = realloc(vector->data, vector->item_count * vector->item_size);
	if (temp == NULL)
		return false;

	vector->data = temp;
	vector->capacity = vector->item_count;

	return true;
}

bool vec_reserve(vec_t* vector, size_t capacity) {
	if (vector == NULL)
		return false;

	if (capacity <= vector->capacity)
		return false;

	if (capacity > SIZE_MAX / vector->item_size)
		return false;

	void* temp = realloc(vector->data, capacity * vector->item_size);
	if (temp == NULL)
		return false;

	vector->data = temp;
	vector->capacity = capacity;

	return true;
}

size_t vec_item_size(const vec_t* vector) {
	if (vector == NULL)
		return 0;

	return vector->item_size;
}

size_t vec_len(const vec_t* vector) {
	if (vector == NULL)
		return 0;

	return vector->item_count;
}
