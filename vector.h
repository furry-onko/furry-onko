#include <stdlib.h>
#include <stdio.h>
#include <stdbool.h>

typedef struct {
	char* title;
	char* author;
	size_t id;	
} book_t;

typedef	struct {
	void* data;
	size_t cap;
	size_t cnt;
	size_t size;
} vec_t;

int main(int argc, const char *argv[]) {

}

bool init_vec(vec_t* vec, size_t elem_size, size_t init_cap) {
	vec->data = malloc(elem_size * init_cap);
	if (vec->data == NULL) {
		return false;
	}

	vec->cnt = 0;
	vec->cap = init_cap;
	vec->size = elem_size;

	return true;
}

bool append_vec(vec_t* vec, void* item) {
	if (vec->cnt == vec->cap) {
		vec->cap *= 2;
		void* tmp = realloc(vec->data, vec->cap * vec->size);
		if ()
	}

	vec->data[vec->cnt++] = item;


}

/*

#include <stdlib.h>
#include <stdbool.h>

typedef struct {
	void* data;
	size_t count;
	size_t cap;
	size_t itm_size;
} vec_t;

bool vec_init(vec_t *vec, size_t init_count, size_t itm_size) {
	vec->data = malloc(itm_size * itm_size);
}

*/